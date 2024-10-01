import React from "react";
import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { FaEdit, FaPlus } from "react-icons/fa";
import MedicalHistoryForm from "../../medicalHistory/MedicalHistoryForm";

type Props = {
  petId: number;
  medicalHistoryId?: number;
  existingData?: any;
  create?: boolean;
};

function DialogMedicalHistoryForm({
  petId,
  existingData,
  medicalHistoryId,
  create,
}: Props) {
  const [open, setOpen] = React.useState(false);

  const closeDialog = () => {
    setOpen(false);
  };

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        {create == true ? (
          <Button variant="outline">
            <FaPlus color="red" />
          </Button>
        ) : (
          <Button variant="outline">
            <FaEdit color="green" />
          </Button>
        )}
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          {create == true ? (
            <DialogTitle>Agregar historial médico</DialogTitle>
          ) : (
            <DialogTitle>Modificar historial médico</DialogTitle>
          )}
        </DialogHeader>
        {create == true ? (
          <MedicalHistoryForm
            petId={petId}
            onCloseDialog={closeDialog}
            create={create}
          />
        ) : (
          <MedicalHistoryForm
            medicalHistoryId={medicalHistoryId}
            existingData={existingData}
            petId={petId}
            onCloseDialog={closeDialog}
            create={create}
          />
        )}
        <DialogFooter></DialogFooter>
      </DialogContent>
    </Dialog>
  );
}

export default DialogMedicalHistoryForm;
