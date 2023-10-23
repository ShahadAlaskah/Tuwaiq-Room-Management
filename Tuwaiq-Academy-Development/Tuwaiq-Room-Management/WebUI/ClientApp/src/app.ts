import './resources/css/index.css';
import {setupAxiosMiddleware} from './config/axiosConfig';
import '@purge-icons/generated';
import Alpine from './config/alpineConfig';
import 'flowbite';
import layout from './components/layout';
import home from './components/home';
import fromTemplateCategories from './components/fromTemplateCategories';
// import registerForm from './components/createForm/registerForm';
import {AlpineComponent} from "alpinejs";
import fromTemplateSections from "./components/fromTemplateSections";
import {toastError} from "./config/toastifyConfig";
import formTemplateIndex from "./components/formTemplateForms/formTemplateIndex";
import createFormTemplate from "./components/formTemplateForms/createFormTemplate";
import updateFormTemplate from "./components/formTemplateForms/updateFormTemplate";


setupAxiosMiddleware();

const components: AlpineComponent[] = [
    ['layout', layout],
    ['home', home],
    ['fromTemplates', formTemplateIndex],
    ['createFormTemplate', createFormTemplate],
    ['updateFormTemplate', updateFormTemplate],
    ['fromTemplateCategories', fromTemplateCategories],
    ['fromTemplateSections', fromTemplateSections],
    // ['registerForm', registerForm],
    // ['publishForm', publishForm],
]

components.forEach(async component => {
    Alpine.data(`${component[0]}Data`, component[1]);
});

const error = document.querySelector('.validation-summary-errors')
if (error) {
    const errorText = error.querySelector('ul li')?.textContent;
    if (errorText) {
        toastError(errorText);
    }
}

// Alpine.data('layoutData', layout);


Alpine.start();