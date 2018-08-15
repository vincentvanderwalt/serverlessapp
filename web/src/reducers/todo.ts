import createReducer from './createReducer';
import { ActionPayload, ActionType, Todo } from '../model/model';
import { TodoAction } from '../actions/todo';

export type ListTodos = Todo[];

export interface TodoState {
    todos: ListTodos;
    loading: boolean;
    error: boolean;
}

export const todoList = createReducer([], {
    [ActionType.TODO_FETCH_COMPLETE](state: Array<Todo>, action: TodoAction) {
        state = action.todos;
        return state;
    },
    [ActionType.TODO_ADD_COMPLETE](state: Array<Todo>, action: TodoAction) {
        return [...state, action.todos];
    },
    [ActionType.ADD_TODO](state: Array<Todo>, action: ActionPayload<Todo>) {
        return [...state, action.payload];
    },
    [ActionType.COMPLETE_TODO](state: Array<Todo>, action: ActionPayload<number>) {
        // search after todo item with the given id and set completed to true
        return state.map(t => t.id === action.payload ? { ...t, completed: true } : t);
    },
    [ActionType.UNCOMPLETE_TODO](state: Array<Todo>, action: ActionPayload<number>) {
        // search after todo item with the given id and set completed to false
        return state.map(t => t.id === action.payload ? { ...t, completed: false } : t);
    },
    [ActionType.DELETE_TODO](state: Array<Todo>, action: ActionPayload<number>) {
        // remove all todos with the given id
        return state.filter(t => t.id !== action.payload);
    },
});
