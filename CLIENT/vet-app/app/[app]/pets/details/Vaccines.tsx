"use client";
import React from "react";
import { Card, CardFooter, CardHeader } from "@/components/ui/card";
import { Button } from "@/components/ui/button";

import { Vaccine } from "@/types";
import { useToast } from "@/components/ui/use-toast";
import useStore from "@/store/store";
import { createInstance } from "@/utils/axiosConfig";
import { useQuery } from "@tanstack/react-query";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { FaTrash } from "react-icons/fa";

type Props = {
  id: number;
};

export default function Vaccines({ id }: Props) {
  const { toast } = useToast();
  const token = useStore((state) => state.token);

  const axiosI = createInstance(token());

  const { data, isLoading, isError } = useQuery({
    queryKey: ["petVaccines"],
    queryFn: async () => {
      const { data } = await axiosI.get(`api/vaccine/pet/${id}`);
      return data;
    },
  });

  const { data: missingData, isLoading: missingDataLoading } = useQuery({
    queryKey: ["petMissingVaccines"],
    queryFn: async () => {
      const { data } = await axiosI.get(`api/vaccine/notpet/${id}`);
      return data;
    },
  });

  const queryClient = useQueryClient();

  const { mutate: addVaccine } = useMutation({
    mutationFn: async ({ petId, vaccineId }) =>
      await axiosI.post("api/vaccine/pet-vaccine", {
        petId: Number(id),
        vaccineId,
      }),
    onSuccess: () => {
      toast({ description: "Vacuna agregada" });
      queryClient.invalidateQueries(["petVaccines"], ["petMissingVaccines"]);
    },
    onError: () => {
      toast({ description: "Error" });
    },
  });

  const { mutate: deleteVaccine, isLoading: deleteLoading } = useMutation({
    mutationFn: async (vaccineId: number) =>
      await axiosI.delete(
        `api/vaccine/pet-vaccine?petId=${id}&vaccineId=${vaccineId}`,
      ),
    onSuccess: () => {
      toast({ description: "Vacuna eliminada" });
      queryClient.invalidateQueries(["petVaccines"]);
    },
    onError: () => {
      toast({ description: "Error" });
    },
  });

  if (isLoading) return <p>Loading...</p>;
  if (missingDataLoading) return <p>Loading..</p>;
  if (isError) return <div>There was an error...</div>;

  return (
    <div className="flex flex-col gap-4">
      {data.map((vaccine: Vaccine) => (
        <Card key={vaccine.id} className="w-[400px] border-green-500">
          <CardHeader>
            <div>{vaccine.name}</div>
            <div>{vaccine.required ? "Obligatoria" : "Opcional"}</div>
            <div>{vaccine.sideEffects}</div>
          </CardHeader>
          <CardFooter>
            <div>
              <Button
                isLoading={deleteLoading}
                onClick={() => deleteVaccine(vaccine.id)}
              >
                <FaTrash color="red" />
              </Button>{" "}
            </div>
          </CardFooter>
        </Card>
      ))}

      <>
        {missingData.map((vaccine: Vaccine) => (
          <Card key={vaccine.id} className="w-[400px]">
            <CardHeader>
              <div>{vaccine.name}</div>
              <div>{vaccine.required ? "Obligatoria" : "Opcional"}</div>
              <div>{vaccine.sideEffects}</div>
            </CardHeader>
            <CardFooter>
              <div>
                <Button
                  onClick={() => addVaccine({ id: id, vaccineId: vaccine.id })}
                >
                  +
                </Button>
              </div>
            </CardFooter>
          </Card>
        ))}
      </>
    </div>
  );
}
