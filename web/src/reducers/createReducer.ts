/**
 * Created by toni on 12.03.2017.
 */
import { ActionPayload } from '../model/model';

export default function createReducer(initialState: Object, handlers: Object) {
    return function reducer(state: Object = initialState, action: ActionPayload<any>) {
        if (handlers.hasOwnProperty(action.type)) {
            return handlers[action.type](state, action);
        } else {
            return state;
        }
    };
}