import Choices from "choices.js";

export interface IChoices {
    icon?: string;
    value: string;
    label: string ;
    // isSelected?: boolean | undefined;
    // isDisabled?: boolean | undefined;
    // groupId?: number | undefined;
    // customProperties?: Record<string, any> | undefined;
    // placeholder?: boolean | undefined;
    // keyCode?: number | undefined;
}

export const choicesSelect = (element: HTMLSelectElement, choices: IChoices[] = [{
    value: '',
    label: ''
}], searchEnabled = true, allowMultiple = false) => {

    return new Choices(
        element,
        {
            choices: choices,
            silent: true,
            placeholder: true,
            maxItemCount: allowMultiple ? 1000 : 1,
            prependValue: null,
            itemSelectText: '',
            allowHTML: false,
            searchEnabled: searchEnabled,
            appendValue: null,
            searchFields: ['label', 'value'],
            duplicateItemsAllowed: false,
            noResultsText: 'لا يوجد نتائج',
            noChoicesText: 'لا يوجد خيارات',
            searchPlaceholderValue: '',
            shouldSort: false,
            callbackOnCreateTemplates: function (template) {
                return {
                    item: ({classNames}, data) => {
                        return template(`
						  <div class="flex gap-2 ${classNames.item} ${data.highlighted
                            ? classNames.highlightedState
                            : classNames.itemSelectable
                        } ${data.placeholder ? classNames.placeholder : ''
                        }" data-item data-id="${data.id}" data-value="${data.value}" ${data.active ? 'aria-selected="true"' : ''
                        } ${data.disabled ? 'aria-disabled="true"' : ''}>
							${data.customProperties.icon ? `<img src="/icons/${data.customProperties.icon}.svg" />` : ''}
							 ${data.label}
						  </div>
						`);
                    },
                    // choice: ({ classNames }, data) => {
                    // 	return template(`
                    // 	<div class="flex gap-2 ${classNames.item} ${data.highlighted
                    // 			? classNames.highlightedState
                    // 			: classNames.itemSelectable
                    // 		} ${data.placeholder ? classNames.placeholder : ''
                    // 		}">
                    // 	${data.customProperties.icon ? `<img src="/icons/${data.customProperties.icon}.svg" class=""/>` : ''}
                    // 	 ${data.label}
                    //   </div>
                    // 	`);
                    // },
                    containerInner: ({classNames}) => {
                        return template(`
							<div class="${classNames.containerInner}" id="${element.name}">
							</div>
						`);
                    }
                };
            }

        }
    );

};


