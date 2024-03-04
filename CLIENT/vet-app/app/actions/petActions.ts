import { Pet } from "@/types";
import { createInstance } from "./axiosConfigComponents";

export const instanceSSR = createInstance();

export async function getAllPets(): Promise<Pet> {
  const res = await instanceSSR.get("api/pet/pets");
  return res.data;
}

export async function getPet(id: number) {
  const res = await instanceSSR.get(`api/pet/${id}`);
  console.log(res.data);
  return res.data;
}

export async function getPetMedicalHistory(id: number) {
  const res = await instanceSSR.get(`api/medicalhistory/pet/${id}`);
  console.log(res.data);
  return res.data;
}
