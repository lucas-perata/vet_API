"use client";
import React from "react";
import { useRouter } from "next/navigation";
import useStore from "../store/store";

const withAuth = (WrappedComponent: React.ComponentType) => {
  const Wrapper: React.FC = (props) => {
    const router = useRouter();
    const token = useStore((state) => state.token);

    React.useEffect(() => {
      if (!token) {
        router.replace("/sessions/login");
      }
    }, [token, router]);

    return <WrappedComponent {...props} />;
  };

  return Wrapper;
};

export default withAuth;

