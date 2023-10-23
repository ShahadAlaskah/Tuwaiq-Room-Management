import Sortable from "sortablejs";

export const createSortable = (element: HTMLElement, draggable?: string | null, handle?: string | null) => {
    return Sortable.create(element, {
        sort: true,
        handle: handle ?? '.drag-handle',
        draggable: draggable ?? '.hand',
        animation: 150,
        filter: ".ignore-drag",
        easing: "cubic-bezier(1, 0, 0, 1)",
        swapThreshold: 1,
        invertSwap: false,
        invertedSwapThreshold: 1
        // onStart: () => {
        //     // Set the height of the placeholder to prevent layout jumps
        //     const parentHeight = element.clientHeight;
        //     element.style.minHeight = parentHeight + 'px';
        // },
        // onEnd: () => {
        //     console.log("qweqw")
        //     // Reset the height of the parent after sorting ends
        //     element.style.minHeight = '';

        //     const sortedIds: string[] = [];
        //     for (let i = 0; i < element.children.length; i++) {
        //         const sortableItem = element.children[i];
        //         if (sortableItem.id) {
        //             const id = sortableItem.id.split("_")[0];
        //             sortedIds.push(id);
        //         }
        //     }

        //     // sort combonent array based on sortedIds
        //     component.sort((a, b) => sortedIds.indexOf(a.id) - sortedIds.indexOf(b.id));
        //     alert("on end")
        // },
        // onUpdate: function (evt) {
        //     console.log("qweqw")
        //     // Reset the height of the parent after sorting ends
        //     element.style.minHeight = '';

        //     const sortedIds: string[] = [];
        //     for (let i = 0; i < element.children.length; i++) {
        //         const sortableItem = element.children[i];
        //         if (sortableItem.id) {
        //             const id = sortableItem.id.split("_")[0];
        //             sortedIds.push(id);
        //         }
        //     }

        //     // sort combonent array based on sortedIds
        //     component.sort((a, b) => sortedIds.indexOf(a.id) - sortedIds.indexOf(b.id));
        // },,
        , onMove: function (evt) {
            const item = evt.dragged;
            const fromParent = item.parentElement;
            const toParent = evt.to;

            if (fromParent !== toParent) {
                return false;
            }
        },
    });
}