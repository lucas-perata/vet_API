import axios from "axios";

export const createInstance = (token: string) => {
  const instance = axios.create({
    baseURL: "http://localhost:5193"
  });

  instance.defaults.headers.common["Authorization"] = `Bearer ${token}`;

  instance.interceptors.request.use(
    (config) => {
      if (token) {
        config.headers["Authorization"] = `Bearer ${token}`;
      }
      return config;
    },
    (error) => {
      return Promise.reject(error);
    }
  );

  return instance;
};

export const axiosConfig = axios.create({
    baseURL: "http://localhost:5193"
})

