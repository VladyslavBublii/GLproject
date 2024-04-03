import { createReducer, on } from '@ngrx/store';
import * as userActions from './user.actions';

export interface UserState {
  id: string;
  email: string;
}

export const initialState: UserState = {
  id: '',
  email: '',
};

export const userReducer = createReducer(
  initialState,

  on(userActions.setUser, (state, { userId, userEmail }) => ({
    ...state,
    id: userId,
    email: userEmail,
  })),
  on(userActions.getUser, (state) => state)
);