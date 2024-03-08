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

import PetExpenseForm from "./PetExpenseForm";

type Props = {
  petId: number;
  expenseId?: number;
  existingData?: any;
  create?: boolean;
};

function DialogPetExpenseForm({
  petId,
  existingData,
  expenseId,
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
            <DialogTitle>Agregar gasto</DialogTitle>
          ) : (
            <DialogTitle>Modificar gasto</DialogTitle>
          )}
        </DialogHeader>
        {create == true ? (
          <PetExpenseForm
            petId={petId}
            onCloseDialog={closeDialog}
            create={create}
          />
        ) : (
          <PetExpenseForm
            expenseId={expenseId}
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

export default DialogPetExpenseForm;
