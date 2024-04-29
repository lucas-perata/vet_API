"use client";
import Cookie from "js-cookie";
import { createInstance } from "@/utils/axiosConfig";
import { Adoption, Pet } from "@/types";

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

export async function createAdoption(formData) {
  return instanceCS
    .post("api/adoption", formData, {
      headers: { "Content-Type": "multipart/form-data" },
    })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
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

export async function deleteExpenseFromPet(expenseId: number) {
  try {
    const res = await instanceCS.delete(`/api/spending/${expenseId}`);
    return res.data;
  } catch (error) {
    console.error("Error deleting expense", error);
    throw error;
  }
}

export async function updateExpense(expenseId: number, data) {
  return instanceCS
    .put(`/api/spending/${expenseId}`, data)
    .then((response) => {
      console.log(response.data);
      return response.data;
    })
    .catch((error) => {
      console.log(error);
      throw error;
    });
}

export async function createExpense(data) {
  return instanceCS
    .post("/api/spending", data)
    .then((response) => {
      console.log(response.data);
      return response.data;
    })
    .catch((error) => {
      console.log(error);
      throw error;
    });
}

export function photoMain(pet: Pet) {
  let photo = pet.petPhoto.map((ph) => ph);
  return photo.find((main) => main.isMain == true)?.url;
}

export function photoMainB(adoption: Adoption) {
  if (adoption.adoptionPhoto == null) return;
  let photo = adoption.adoptionPhoto.map((ph) => ph);
  console.log(photo);
  return photo.find((main) => main.isMain == true)?.url;
}
