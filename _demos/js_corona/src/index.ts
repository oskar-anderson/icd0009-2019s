console.log("Hello from Corona JS");

interface IState {
    name: string;
    title: string;
    [propName: string]: string;
}

let initialState : IState = {
    name: "Andres Käver",
    title: "lecturer"
};

let state = new Proxy(initialState, {
    set(target: IState, propertyName: string, value: string){
        target[propertyName] = value;
        renderUI();
        return true
    }
});


const renderUI = () => {
    const bindings = Array.from(
        document.querySelectorAll('[data-binding]')
        ).map((elem) => (elem as HTMLElement).dataset.binding!);
    bindings.forEach((binding) => {
        document.querySelector(`[data-binding='${binding}']`)!.innerHTML 
            = state[binding];
        (document.querySelector(`[data-model='${binding}']`)! as HTMLInputElement).
            value = state[binding];
    });
}


state.name = "Pavel";
state.title = "Seroi";

// attaching directly to on* is not the best practice
let button = document.querySelector('.js-reset-data') as HTMLAnchorElement;
button.onclick = function () {
    state.title = "foo";
    state.name = "bar";
}

const listeners = Array.from(document.querySelectorAll('[data-model]'));
listeners.forEach( listener => {
    if (listener instanceof HTMLInputElement) {
        const name = listener.dataset.model!;
        // this is correct aproach
        listener.addEventListener('keyup', (event) => {
            state[name] = listener.value;
        })
        listener.addEventListener('change', (event) => {
            state[name] = listener.value;
        })
    }
});
