import type {Configuration} from 'webpack';
import CssMinimizerPlugin from 'css-minimizer-webpack-plugin';
import MiniCssExtractPlugin from 'mini-css-extract-plugin';
import path from 'path';
import PurgeIcons from 'purge-icons-webpack-plugin';
import  TerserPlugin from 'terser-webpack-plugin';
import  ESLintPlugin from 'eslint-webpack-plugin';

const config: Configuration = {
    entry: {
        app: './src/app.ts',
        vendors: ['alpinejs', 'toastify-js', 'tom-select', 'inputmask', 'zod']
    },
    output: {
        filename: '[name]/[name].js',
        path: path.resolve(__dirname, '..', 'wwwroot', 'dist'),
    },
    optimization: {
        runtimeChunk: 'single',
        splitChunks: {
            cacheGroups: {
                vendors: {
                    test: /[\\/]node_modules[\\/]/,
                    name: 'libs',
                    chunks: 'all',
                    enforce: true
                }
            }
        },
        minimize: true,
        minimizer: [new CssMinimizerPlugin(), new TerserPlugin()]
    },
    devtool: 'source-map',
    resolve: {
        extensions: ['.mjs', '.ts', '.js', '.json']
    },
    plugins: [
        new MiniCssExtractPlugin({filename: '[name].css'}),
        new PurgeIcons(),
        new ESLintPlugin({
            files: 'src/**/*.ts'
        })
    ],
    module: {
        rules: [
            {
                test: /\.css$/,
                use: ['style-loader',
                    {
                        loader: MiniCssExtractPlugin.loader,
                        options: {
                            //hmr: argv.mode === 'development'
                        }
                    },
                    {
                        loader: 'css-loader',
                        options: {
                            importLoaders: 1
                        }
                    },
                    'postcss-loader'],
            },
            {
                test: /\.(eot|woff(2)?|ttf|otf|svg)$/i,
                type: 'asset'
            },
            {
                test: /\.[jt]s?$/,
                exclude: /node_modules/,
                use: [
                    {
                        loader: 'ts-loader',
                        options: {
                            compilerOptions: {
                                noEmit: false,
                                target: 'es6',
                                moduleResolution: 'node',
                                noImplicitAny: false,
                                strict: true,
                                esModuleInterop: true,
                                allowJs: true,
                                checkJs: false,
                                resolveJsonModule: true,
                                skipLibCheck: true,
                                forceConsistentCasingInFileNames: true,
                                allowSyntheticDefaultImports: true,
                                typeRoots: ['./node_modules/@types', './src/types']
                            }
                        }
                    }
                ]
            },
            {
                test: /\.m?js/,
                resolve: {
                    fullySpecified: false
                }
            }
        ]
    }
};

export default config;
