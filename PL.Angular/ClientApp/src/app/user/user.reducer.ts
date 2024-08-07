import { createReducer, on } from '@ngrx/store';
import * as userActions from './user.actions';

export interface UserState {
  id: string;
  email: string;
  role: string; 
}

export const initialState: UserState = {
  id: '',
  email: '',
  role: ''
};

export const userReducer = createReducer(
  initialState,
  on(userActions.setUser, (state, { userId, userEmail, userRole }) => ({
    ...state,
    id: userId,
    email: userEmail,
    role: userRole
  })),
  on(userActions.getUser, (state) => state)
);