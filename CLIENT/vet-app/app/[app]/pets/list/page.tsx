import React from "react";
import PetCard from "./PetCard";
import { kookies } from "@/app/actions/kookies";
import FloatingButton from "@/components/ui/FloatingButton";
import { getAllPets } from "@/app/actions/petActions";

async function page() {
  const data = await getAllPets();

  return (
    <div className="flex justify-center gap-5 flex-wrap">
      {data &&
        data.map((pet) => (
          <PetCard
            photo={pet.petPhoto.map((ph) => ph)}
            pet={pet}
            key={pet.id}
          ></PetCard>
        ))}
      <FloatingButton route="/app/pets/add" />
    </div>
  );
}

export default page;
