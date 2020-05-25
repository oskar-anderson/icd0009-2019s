export interface IMeal {
    id: string;
    categoryId: string;
    name: string;
    picture: string;
    description: string;    
}

export interface IMealCreate {
    categoryId: string;
    category: null;
    name: string;
    picture: string;
    description: string; 
}