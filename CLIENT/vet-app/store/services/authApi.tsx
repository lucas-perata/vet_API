import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react"
import {LoginResponse} from "../slices/auth";

export const authApi = createApi({
    reducerPath: "authApi",
    baseQuery: fetchBaseQuery({
        baseUrl: 
            typeof window === "undefined"
            ? "http://localhost:3000"
            : window.location.origin, 
    }),
    endpoints: (builder) => ({
        login: builder.mutation<LoginResponse, {userEmail: string, password: string}>({
            query: ({userEmail, password}) => ({
                url: "http://localhost:5193/login-vet",
                method: "POST",
                body: {
                    userEmail,
                    password
                },
            }),
        }),
        getAuthData: builder.query<LoginResponse, {token:string}> ({
            query: ({token}) => ({
                url: "http://localhost:5193/currentuser", // TODO revisar este endpoint _agregar a la API un endpoint que mande data del usuario incluido su jwt
                method:"GET",
                headers: {
                    Authorization: `Bearer ${token}`,
                }
            })
        })
    })
})

export const {useLoginMutation, useGetAuthDataQuery} = authApi;