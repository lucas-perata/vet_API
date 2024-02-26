import { createSlice } from '@reduxjs/toolkit';
import { serialize } from 'cookie';
import { authApi } from '../services/authApi';
import { removeCookies } from '../../lib/cookies';


export type LoginResponse = {
    token: string; 
    userEmail: string;
    userName: string;
    id: string;
};

const setAuthCookie = (token: string, name: string) => {
    document.cookie = serialize(name, token, {
        secure: process.env.NODE_ENV !== 'development',
        sameSite: 'strict',
        maxAge: 3600,
        path: '/',
      });
}

const initialState: Partial<LoginResponse> = {}; 

const slice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        logout: () => {
            removeCookies("auth_token");
            return {};
        }
    },
    extraReducers: (builder) => {
        builder
            .addMatcher(
                authApi.endpoints.login.matchFulfilled,
                (_state, {payload}) => {
                    setAuthCookie(payload.token, "auth_token");
                    return payload;
                }
            )
            .addMatcher(
                authApi.endpoints.getAuthData.matchFulfilled,
                (_state, {payload}) => {
                    setAuthCookie(payload.token, "auth_token"); 
                    return payload;
                }
            )
    }
}); 

export const authReducer = slice.reducer;
export const { logout } = slice.actions;

