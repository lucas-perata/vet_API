"use client";
import React from "react";
import { Card, CardFooter, CardHeader } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import {
  addVaccineToPetCS,
  deleteVaccineFromPet,
} from "@/app/actions/petActionsCS";
import useFetchData from "@/app/hooks/useFetch";
import { Vaccine } from "@/types";
import { useToast } from "@/components/ui/use-toast";
import useStore from "@/store/store";

type Props = {
  id: number;
};

export default function Vaccines({ id }: Props) {
  const { toast } = useToast();
  const token = useStore((state) => state.token);
  const config = {
    petUrl: `http://localhost:5193/api/Vaccine/pet/${id}`,
    secondaryUrl: `http://localhost:5193/api/Vaccine/notpet/${id}`,
    authorizationToken: token,
  };

  const { data, secondaryData, isLoading, refreshData } = useFetchData(
    id,
    config,
  );

  const add = async (petId: number, vaccineId: number) => {
    await addVaccineToPetCS({ petId, vaccineId });
    toast({
      title: "Vacuna agregada",
    });
    refreshData();
  };

  const remove = async (petId: number, vaccineId: number) => {
    await deleteVaccineFromPet({ petId, vaccineId });
    toast({
      title: "Vacuna eliminada",
    });
    refreshData();
  };

  if (isLoading) return <p>Loading...</p>;
  if (!data) return <p>No profile data</p>;

  return (
    <div className="flex flex-col gap-4">
      <h2>Pet Vaccines</h2>
      {data.map((vaccine: Vaccine) => (
        <Card key={vaccine.id} className="w-[400px] border-green-500">
          <CardHeader>
            <div>{vaccine.name}</div>
            <div>{vaccine.required ? "Obligatoria" : "Opcional"}</div>
            <div>{vaccine.sideEffects}</div>
          </CardHeader>
          <CardFooter>
            <div onClick={() => remove(id, vaccine.id)}>
              <Button>borrar</Button>
            </div>
          </CardFooter>
        </Card>
      ))}

      {secondaryData && (
        <>
          <h2>Missing Vaccines</h2>
          {secondaryData.map((vaccine: Vaccine) => (
            <Card key={vaccine.id} className="w-[400px]">
              <CardHeader>
                <div>{vaccine.name}</div>
                <div>{vaccine.required ? "Obligatoria" : "Opcional"}</div>
                <div>{vaccine.sideEffects}</div>
              </CardHeader>
              <CardFooter>
                <div onClick={() => add(id, vaccine.id)}>
                  <Button>+</Button>
                </div>
              </CardFooter>
            </Card>
          ))}
        </>
      )}
    </div>
  );
}
