'use strict';

import {sayHi} from './sayHi.js'
import {ho} from './utils/ho.js'

alert(sayHi("andres"))
alert(ho("andres"))


if (false) {
    class Person {
        constructor(firstName, lastName){
            this.firstName = firstName;
            this.lastName = lastName;
        }

        getFullName(){
            return this.firstName + '' + this.lastName;
        }

        get fullName() {
            return this.firstName + ' ' + this.lastName;
        }
        
        set fullName(value) {
            this.firstName = value; this.lastName = "-";
        }

    }

    let x = new Person('Andres', 'Käver')
    x.fullName = 'testing'
    alert(x.fullName)
}