import { Card, CardFooter, CardHeader } from "@/components/ui/card";
import React from "react";
import { Button } from "@/components/ui/button";
import Link from "next/link";

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
          <Link href={`historialmedico/${medicalHistory.id}`}>
            <Button>Detalles</Button>
          </Link>
        </CardFooter>
      </Card>
    </>
  );
}

export default MedicalHistoryCard;
