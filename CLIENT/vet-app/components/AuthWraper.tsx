import { useRouter } from "next/router";
import { useDispatch, useSelector } from "react-redux";
import { getValidAuthTokens } from "@/lib/cookies";
import React, { useEffect } from "react";
import { useGetAuthDataQuery } from "@/store/services/authApi";
import { RootState } from "@reduxjs/toolkit/query";
import { logout } from "@/utils/auth";

type Props = {
    children?: React.ReactNode;
};

export const AuthWrapper = ({children}: Props) => {
    const dispatch = useDispatch();
    const {push} = useRouter();
    const { userEmail } = useSelector((state: RootState) => state.auth);
    const {token} = getValidAuthTokens(); 

    const {error, isLoading} = useGetAuthDataQuery(
        {token: token || ''},
        {
            skip: !!userEmail || !token,
        }
    );

    useEffect(()=> {
        if(!token){
            push('/login');
            dispatch(logout());
        }
    }, [token, push]);

    if (isLoading){
        return <div>Loading...</div>
    }
    return children;
}