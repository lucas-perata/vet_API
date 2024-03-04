import React from "react";
import withAuthSSR from "@/utils/withAuthSSR";
import PetCard from "./PetCard";
import { getAllPets } from "../../actions/petActions";
import FloatingButton from "../../../components/ui/FloatingButton";
import { kookies } from "@/app/actions/kookies";

async function page() {
  withAuthSSR(kookies);
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
      <FloatingButton route="/pets/add" />
    </div>
  );
}

export default page;
