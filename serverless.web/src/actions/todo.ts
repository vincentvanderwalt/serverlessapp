import { ActionPayload, ActionType, Todo } from '../model/model';
import { Action, Dispatch } from 'redux';

export type TodoList = Todo[];

export interface TodoAction extends Action {
  todos: TodoList;
}

const todoFetchSuccess = (todos: TodoList) => {
  return {
    type: ActionType.TODO_FETCH_COMPLETE,
    todos: todos
  };
};

export const todoFetch = () => (dispatch: Dispatch<TodoAction>) => {
  fetch('https://todowebappvw.azurewebsites.net/api/GetTodos', {
    method: 'get'
  })
    .then(response => response.json())
    .then(json => {
      return dispatch(todoFetchSuccess(json));
    })
    .catch(error => {
      console.log(`Api Error fetching documents : ${error}`);
    });
};

export function getTodos(): ActionPayload<Todo[]> {
  // fetch('http://localhost:7071/api/GetTodos', {
  //   method: 'get'
  // })
  //   .then(response => response.json())
  //   .then(json => console.log(json))
  //   .catch(error => {
  //     console.log(`Api Error fetching documents : ${error}`);
  //   });

  // console.log(testDocs);

  let todoItem: Todo = { id: 1, text: 'hello', completed: false };
  let test: Todo[] = [];
  test.push(todoItem);
  return {
    type: ActionType.GET_TODOS,
    payload: test
  };
}

export function addTodo(todo: Todo): ActionPayload<Todo> {
  return {
    type: ActionType.ADD_TODO,
    payload: todo
  };
}

// Async Function expample with redux-thunk
export function completeTodo(todoId: number) {
  // here you could do API eg

  return (dispatch: Function, getState: Function) => {
    dispatch({ type: ActionType.COMPLETE_TODO, payload: todoId });
  };
}

export function uncompleteTodo(todoId: number): ActionPayload<number> {
  return {
    type: ActionType.UNCOMPLETE_TODO,
    payload: todoId
  };
}

export function deleteTodo(todoId: number): ActionPayload<number> {
  return {
    type: ActionType.DELETE_TODO,
    payload: todoId
  };
}