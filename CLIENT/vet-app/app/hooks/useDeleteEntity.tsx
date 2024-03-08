import { useToast } from "@/components/ui/use-toast";
import useStore from "@/store/store";
import { createInstance } from "@/utils/axiosConfig";
import { useQueryClient } from "@tanstack/react-query";
import { useMutation } from "@tanstack/react-query";

export function useDeleteEntity(
  url: string,
  queryName: string,
  entityName: string,
) {
  const { toast } = useToast();
  const token = useStore((state) => state.token);
  const axiosI = createInstance(token());
  const queryClient = useQueryClient();

  const { mutate: deleteEntity, isLoading: deleteLoading } = useMutation({
    mutationFn: async (id: number) => await axiosI.delete(url + id),
    onSuccess: () => {
      toast({ description: `${entityName} eliminado` });
      queryClient.invalidateQueries([queryName]);
    },
    onError: () => {
      toast({ description: "No se pudo eliminar" });
    },
  });

  return { deleteEntity, deleteLoading };
}
