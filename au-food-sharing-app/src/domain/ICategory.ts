export interface ICategory {
    id: string;
    name: string;
    forMeal: boolean;
    forPizzaTemplate: boolean;
};

export interface ICategoryCreate {
    name: string;
    forMeal: boolean;
    forPizzaTemplate: boolean;
};