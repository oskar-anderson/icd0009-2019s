export function getElementById(id: string): HTMLElement {
    const el: HTMLElement | null = document.getElementById(id);
    if (el === null) {
        throw new Error(id + ' not found');
    }
    return el;
}

export function getElementFromQuery(el: Element | Document, id: string): HTMLElement {
    const subEl: HTMLElement | null = el.querySelector(id);
    if (subEl === null) {
        throw new Error(id + ' not found');
    }
    return subEl;
}

export function getClassListClass(el: HTMLElement, idx: number): string {
    if (el.classList.length < idx || idx < 0) {
        throw new Error(idx + ' invalid');
    }
    const subEl: string | null = el.classList.item(idx);
    if (subEl === null) {
        throw new Error('Can in range HTMLElement classlist element be null?');
    }
    return subEl;
}

export function getElementsByName(id: string): Array<HTMLInputElement> {
    const el: Array<HTMLInputElement> | null = Array.prototype.slice.call(document.getElementsByName(id));
    if (el === null) {
        throw new Error(id + ' not found');
    }
    return el;
}
