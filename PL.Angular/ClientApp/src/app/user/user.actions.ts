import { createAction, props } from '@ngrx/store';

export const getUser = createAction('[User] Get User');
export const setUser = createAction('[User] Set User', props<{ userId: string, userEmail: string, userRole: string }>());