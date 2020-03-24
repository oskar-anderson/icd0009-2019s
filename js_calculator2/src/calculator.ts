export class Calculator {
    
    private _display : string = "0";
    constructor(){
        this._display = "0";
    }

    handleKey(key : string) {
        let num = Number(key);
        if (!isNaN(num)){
            this._numberPressed(num);
        }
        return this._display;
    }
    
    _numberPressed(num : number){
        if (this._display == "0"){
            this._display = num.toString();
        } else {
            this._display += num.toString();
        }
    }


    get display(){
        return this._display;
    }

}
