"use client";
import Cookie from "js-cookie";
import { createInstance } from "@/utils/axiosConfig";

const kookiesCS = Cookie.get("token");
const instanceCS = createInstance(kookiesCS);

export function createPet(data) {
  return instanceCS
    .post("api/pet", data)
    .then((response) => {
      console.log(response.data);
      return response.data;
    })
    .catch((error) => {
      console.log(error);
      throw error; // Rethrow the error to propagate it to the caller
    });
}
