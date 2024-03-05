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

export async function addVaccineToPetCS({ petId, vaccineId }) {
  try {
    const res = await instanceCS.post("api/vaccine/pet-vaccine", {
      petId,
      vaccineId,
    });
    console.log("Vaccine relationship added");
    return res.data;
  } catch (error) {
    console.error("Error adding vaccine to pet:", error);
    throw error; // re-throw the error so it can be handled by the caller
  }
}
