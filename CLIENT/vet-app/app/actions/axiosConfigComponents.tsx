import axios from "axios";
import { cookies } from "next/headers";

export const createInstance = () => {
  const instance = axios.create({
    baseURL: "http://localhost:5193",
  });

  instance.interceptors.request.use(
    (config) => {
      if (cookies().get("token")?.value) {
        config.headers["Authorization"] =
          `Bearer ${cookies().get("token")?.value}`;
      }
      return config;
    },
    (error) => {
      return Promise.reject(error);
    },
  );

  return instance;
};

export const axiosConfig = axios.create({
  baseURL: "http://localhost:5193",
});
