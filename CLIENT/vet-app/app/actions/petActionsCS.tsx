"use client";
import Cookie from "js-cookie";
import { createInstance } from "@/utils/axiosConfig";

const kookiesCS = Cookie.get("token");
const instanceCS = createInstance(kookiesCS);

export async function createPet(data) {
  return instanceCS
    .post("api/pet", data)
    .then((response) => {
      console.log(response.data);
      return response.data;
    })
    .catch((error) => {
      console.log(error);
      throw error;
    });
}

export async function addVaccineToPetCS({
  petId,
  vaccineId,
}: {
  petId: number;
  vaccineId: number;
}) {
  try {
    const res = await instanceCS.post("api/vaccine/pet-vaccine", {
      petId,
      vaccineId,
    });
    console.log("Vaccine relationship added");
    return res.data;
  } catch (error) {
    console.error("Error adding vaccine to pet:", error);
    throw error;
  }
}

export async function deleteVaccineFromPet({
  petId,
  vaccineId,
}: {
  petId: number;
  vaccineId: number;
}) {
  try {
    const res = await instanceCS.delete(
      `/api/vaccine/pet-vaccine?petId=${petId}&vaccineId=${vaccineId}`,
    );
    return res.data;
  } catch (error) {
    console.error("Error deleting vaccine from pet: ", error);
    throw error;
  }
}
