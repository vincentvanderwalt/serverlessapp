
export interface Todo {
    id: number;
    text: string;
    completed: boolean;    
}

export enum ActionType {
    ADD_TODO,
    DELETE_TODO,
    COMPLETE_TODO,
    UNCOMPLETE_TODO,
    GET_TODOS,
    TODO_FETCH_COMPLETE,
    TODO_ADD_COMPLETE
}

export interface ActionPayload<T> {
    type: ActionType;
    payload: T;
}