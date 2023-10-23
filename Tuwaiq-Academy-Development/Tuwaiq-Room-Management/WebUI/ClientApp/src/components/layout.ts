import { AlpineComponent } from 'alpinejs';
import { myAxios } from '../config/axiosConfig';
import { api } from '../utils/endpoints';
import { AxiosResponse } from "axios";

export interface ILayout extends AlpineComponent {
    route: {
        url: string;
        name: string;
    };
    isLoading: boolean;
    init: () => Promise<void>;
    getRouteUrl: string;
    getRouteName: string;
    openUser: boolean;
    navbar: {
        isOpen: boolean;
        isAcademyOpen: boolean;
        isProfileOpen: boolean;

    };
    toggleMobileNavbar: () => void;
    toggleAcademyNavbar: () => void;
    toggleProfileNavbar: () => void;
    menu: {
        forms: {
            home: number;
            info: number;
        }
    };
    setCurrentRoute: (url: string, name: string) => void;

    uploadFile: (files: File[]) => Promise<AxiosResponse<any, any> | null>;

}


const layoutComponent: ILayout = {
    menu: { forms: { home: 0, info: 0 } },
    toggleAcademyNavbar(): void {
        //
    },
    toggleProfileNavbar(): void {
        //
    },
    route: {
        url: '',
        name: '',
    },
    isLoading: true,
    openUser: false,
    navbar: {
        isOpen: false,
        isAcademyOpen: false,
        isProfileOpen: false,
    },
    currentUser: {
        id: "",
        name: "",
        email: "",
        username: "",
    },
    async init() {
        // await Promise.all([
        //     this.initUserData(),
        // ]);
        this.setCurrentRoute('index', document.title);

        this.isLoading = false;
    },
    get getRouteUrl() {
        return this.route.url;
    },
    get getRouteName() {
        return this.route.name;
    },
    setCurrentRoute(url: string, name: string) {
        this.route.url = url;
        this.route.name = name;
    },
    toggleMobileNavbar() {
        this.navbar.isOpen = !this.navbar.isOpen;
    },
    toggleAcademyMenu() {
        this.navbar.isAcademyOpen = !this.navbar.isAcademyOpen;
    },
    toggleProfileMenu() {
        this.navbar.isProfileOpen = !this.navbar.isProfileOpen;
    },
    async initUserData() {
        //
    }
    ,
    async uploadFile(files: File[]) {
        if (files.length > 0) {
            this.isLoading = true;
            const fd: FormData = new FormData();

            Array.from(files).forEach((file) => {
                fd.append("files", file);
            });

            const result = await myAxios.post(api.Upload, fd, {
                headers: {
                    "Content-Type": "multipart/form-data",
                },
            });
            this.isLoading = false;
            return result;
        } else {
            return null;
        }
    },
};

export default () => layoutComponent;
