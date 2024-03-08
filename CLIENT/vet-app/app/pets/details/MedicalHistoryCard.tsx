"use client";
import { Card, CardFooter, CardHeader } from "@/components/ui/card";
import React from "react";
import DialogMedicalHistory from "./DialogMedicalHistory";
import { MedicalHistory } from "@/types";
import useStore from "@/store/store";
import { createInstance } from "@/utils/axiosConfig";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { toast } from "@/components/ui/use-toast";
import { Button } from "@/components/ui/button";
import { FaTrash } from "react-icons/fa";
import { useDeleteEntity } from "@/app/hooks/useDeleteEntity";
import DialogMedicalHistoryForm from "./DialogMedicalHistoryForm";

type Props = {
  id: number;
};

function MedicalHistoryCard({ id }: Props) {
  const token = useStore((state) => state.token);

  const axiosI = createInstance(token());

  const { data, isLoading, isError } = useQuery({
    queryKey: ["medicalHistory"],
    queryFn: async () => {
      const { data } = await axiosI.get(`api/medicalhistory/pet/${id}`);
      return data;
    },
  });

  const { deleteEntity, deleteLoading } = useDeleteEntity(
    "api/medicalhistory/",
    "medicalHistory",
    "Historial médico",
  );

  if (isLoading) return <p>Loading...</p>;
  if (isError) return <p>There was an error...</p>;

  return (
    <>
      <h2 className="scroll-m-20 pb-2 text-3xl font-semibold tracking-tight first:mt-0 self-center flex items-center gap-4">
        Historial médico
        <DialogMedicalHistoryForm create={true} petId={id} />
      </h2>

      {data.map((medicalHistory: MedicalHistory) => (
        <Card className="w-[400px]">
          <CardHeader>
            <div>{medicalHistory.description}</div>
            <div>{medicalHistory.date}</div>
            <div>{medicalHistory.id}</div>
          </CardHeader>
          <CardFooter>
            <DialogMedicalHistory data={medicalHistory} />
            <div>
              <Button
                isLoading={deleteLoading}
                onClick={() => deleteEntity(medicalHistory.id)}
              >
                <FaTrash color="red" />
              </Button>
              <DialogMedicalHistoryForm
                create={false}
                petId={id}
                medicalHistoryId={medicalHistory.id}
                existingData={medicalHistory}
              />
            </div>
          </CardFooter>
        </Card>
      ))}
    </>
  );
}

export default MedicalHistoryCard;
