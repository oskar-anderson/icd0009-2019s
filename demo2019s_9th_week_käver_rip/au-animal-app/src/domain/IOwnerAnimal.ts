import { IAnimal } from "./IAnimal";
import { IOwner } from "./IOwner";

export interface IOwnerAnimal {
    id: string;
    
    animalId: string;
    animal: IAnimal;

    ownerId: string;
    owner: IOwner;
}
