import { ActionType, Todo } from '../model/model';
import { Action, Dispatch } from 'redux';

export type ToDoList = Todo[];

export interface TodoAction extends Action {
  todos: ToDoList;
}

export const todoFetch = () => (dispatch: Dispatch<TodoAction>) => {

  // API request will be executed...
  // dispatch(postsFetchBegin());

  fetch('http://localhost:7071/api/GetTodos', {
    method: 'get'
  })
    .then(response => Promise.all([response.json()]))
    .then(json => {
      dispatch(fetchTodosCompleted(json[0]));
    })
    .catch(error => {
      console.log(`Api Error fetching documents : ${error}`);
    });

  // ...now
  // return axios.get(postsApiUrl)
  //     .then((response: { data: PostsList }) => {

  //         // Get only 10 first posts, due to task requirements
  //         // Array should be sliced ASAP, because we don't need large amount of data in an action
  //         const first10Posts = response.data.slice(0, 10);

  //         dispatch(postsFetchSuccess(first10Posts));
  //     })
  //     .catch(() => {

  //         // Something is no yes 👎 (thanks Tusk for english lessons)
  //         dispatch(postsFetchError());
  //     });
};


export const fetchTodos = () => (dispatch:any) => {
  fetch('http://localhost:7071/api/GetTodos', {
    method: 'get'
  })
    .then(response => Promise.all([response.json()]))
    .then(json => {
      dispatch(fetchTodosCompleted(json[0]));
    })
    .catch(error => {
      console.log(`Api Error fetching documents : ${error}`);
    });
}



export function getTodos(): Action<Array<Todo>> {
  var todoItems = Array<Todo>();
  // fetch('http://localhost:7071/api/GetTodos', {
  //   method: 'get'
  // })
  //   .then(response => Promise.all([response.json()]))
  //   .then(json => {
  //     dispatch(fetchMembersCompleted(members));
  //   })
  //   .catch(error => {
  //     console.log(`Api Error fetching documents : ${error}`);
  //   });

  //   Promise.all(getItems)

  console.log(todoItems);

  return {
    type: ActionType.GET_TODOS,
    payload: todoItems
  };

  // let todoItem: Todo = { id: 1, text: 'hello', completed: false };
  // let test: Todo[] = [];
  // test.push(todoItem);

}

const fetchTodosCompleted = (todos: Todo[]) => ({
  type: ActionType.GET_TODOS,
  payload: todos
});

export function addTodo(todo: Todo): Action<Todo> {
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

export function uncompleteTodo(todoId: number): Action<number> {
  return {
    type: ActionType.UNCOMPLETE_TODO,
    payload: todoId
  };
}

export function deleteTodo(todoId: number): Action<number> {
  return {
    type: ActionType.DELETE_TODO,
    payload: todoId
  };
}
