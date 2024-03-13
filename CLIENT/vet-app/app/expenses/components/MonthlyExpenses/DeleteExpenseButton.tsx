import { useDeleteEntity } from "@/app/hooks/useDeleteEntity";
import { Button } from "@/components/ui/button";
import React from "react";
import { FaTrash } from "react-icons/fa";

function DeleteExpenseButton({ id }) {
  const { deleteEntity, deleteLoading } = useDeleteEntity(
    "api/spending/",
    "monthlyExpenses",
    "Gasto",
  );
  return (
    <Button
      onClick={() => deleteEntity(id)}
      isLoading={deleteLoading}
      disabled={deleteLoading}
    >
      <FaTrash color="red" />
    </Button>
  );
}

export default DeleteExpenseButton;
