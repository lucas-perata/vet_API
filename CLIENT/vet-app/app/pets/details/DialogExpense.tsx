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
import ExpenseForm from "@/app/expenses/ExpenseForm";
import { FaPlus } from "react-icons/fa";

type Props = {
  petId: number;
};

function DialogExpense({ petId }: Props) {
  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button variant="outline">
          <FaPlus />
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Crear gasto</DialogTitle>
        </DialogHeader>
        <ExpenseForm petId={petId} />
        <DialogFooter></DialogFooter>
      </DialogContent>
    </Dialog>
  );
}

export default DialogExpense;
