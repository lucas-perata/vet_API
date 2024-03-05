import { Card, CardFooter, CardHeader } from "@/components/ui/card";
import React from "react";
import AddVaccineToPetButton from "./[id]/AddVaccineToPetButton";
import { GetVaccinesForPet } from "@/app/actions/petActions";

type Props = {
  vaccine: any;
  petId: number;
  button: any;
};

async function VaccineCard({ vaccine, petId, button }: Props) {
  return (
    <>
      <Card className="w-[400px]">
        <CardHeader>
          {petId}
          <div>{vaccine.name}</div>
          <div>{vaccine.isRequired == true ? "Obligatoria" : "Opcional"}</div>
          <div>{vaccine.sideEffects}</div>
        </CardHeader>
        <CardFooter>
          <AddVaccineToPetButton
            petId={petId}
            vaccineId={vaccine.id}
            button={button}
          />
        </CardFooter>
      </Card>
    </>
  );
}

export default VaccineCard;
