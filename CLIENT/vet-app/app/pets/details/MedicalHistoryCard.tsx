import { Card, CardFooter, CardHeader } from "@/components/ui/card";
import React from "react";
import DialogMedicalHistory from "./DialogMedicalHistory";

type Props = {
  medicalHistory: any;
};

function MedicalHistoryCard({ medicalHistory }: Props) {
  return (
    <>
      <Card className="w-[400px]">
        <CardHeader>
          <div>{medicalHistory.description}</div>
          <div>{medicalHistory.date}</div>
          <div>{medicalHistory.id}</div>
        </CardHeader>
        <CardFooter>
          <DialogMedicalHistory params={{ id: medicalHistory.id }} />
        </CardFooter>
      </Card>
    </>
  );
}

export default MedicalHistoryCard;
