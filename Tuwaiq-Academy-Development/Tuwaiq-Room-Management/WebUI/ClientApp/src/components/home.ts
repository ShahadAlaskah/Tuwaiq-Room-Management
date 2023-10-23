import {ILayout} from './layout';

export interface IHome extends Partial<ILayout> {
    init: () => Promise<void>;
}

const home: IHome = {
    table: null,
    async init() {
        this.setCurrentRoute!('index', document.title);
    },
};

export default () => home;
