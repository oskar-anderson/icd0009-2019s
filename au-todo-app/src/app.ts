import { ITodo } from 'domain/ITodo';
import { EventAggregator, Subscription } from 'aurelia-event-aggregator';
import { autoinject } from 'aurelia-framework';
import { EventChannels } from 'types/EventChannels';
import { AppState } from 'state/app-state';


@autoinject
export class App {
    private _subscriptions: Subscription[] = []
    private _todos: ITodo[] = [];
    
    private _placeholder = "what?";
    private _appTitle = "Aurelia Todos";
    private _submitButtonTitle = "Add";
    private _input = "";

    constructor(private appState: AppState, private eventAggregator: EventAggregator) {
        /*
        this._subscriptions.push(
            this.eventAggregator.subscribe
            (EventChannels.NewTodoCreation,
            (description: string) => this.eventNewTodoCreation(description))

        );
        */
    }

    detached() {
        this._subscriptions.forEach(subscription => subscription.dispose());
        this._subscriptions = [];
    }

    /*
    eventNewTodoCreation(description: string) {
        this._todos.push({ description: description, done: false})
    }
    */

    formSubmitted(event: Event) {
        if (this._input.length > 0) {
            this._todos.push({description: this._input, done: false});
        }
        this._input = "";
        console.log(this._todos);
    }

    removeTodo(index: number) {
        // console.log(index);
        this.appState.removeTodo(index);
        // this._todos.splice(index, 1);
    }

    getDate(): string {
        return new Date().getTime().toString();
    }
}
