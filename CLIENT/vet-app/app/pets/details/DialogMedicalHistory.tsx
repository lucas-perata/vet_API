"use client";
import React from "react";
import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { MedicalHistory } from "@/types";

type Props = {
  data: MedicalHistory;
};

function DialogMedicalHistory({ data }: Props) {
  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button variant="outline">Detalles</Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle className="">Consulta del {data.date}</DialogTitle>
          <DialogDescription>Atendido por {data.vetName}</DialogDescription>
        </DialogHeader>
        <div className="flex items-center gap-3">
          <h2 className="scroll-m-20 pb-2 text-xl font-semibold tracking-tight first:mt-0 self-center">
            Motivo de la consulta:
          </h2>
          {data.description}
        </div>
        <div className="flex items-center gap-3">
          <h2 className="scroll-m-20 pb-2 text-xl font-semibold tracking-tight first:mt-0 self-center">
            Tratamiento indicado
          </h2>
          {data.treatment}
        </div>
        <div className="grid gap-4 py-4"></div>
        <DialogFooter></DialogFooter>
      </DialogContent>
    </Dialog>
  );
}

export default DialogMedicalHistory;
