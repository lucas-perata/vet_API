import React from "react";
import PetForm from "../PetForm";

function addPet() {
  return (
    <div className="mx-auto max-w-6xl shadow-lg p-10 bg-white rounded-lg">
      <h1 className="text-2xl font-extrabold tracking-tight">
        Agregá a tu mascota
      </h1>
      <div className="">
        <PetForm></PetForm>
      </div>
    </div>
  );
}

export default addPet;
