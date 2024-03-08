import useStore from "@/store/store";
import { createInstance } from "@/utils/axiosConfig";
import { zodResolver } from "@hookform/resolvers/zod";
import { useQueryClient, useMutation } from "@tanstack/react-query";
import React, { useEffect } from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
} from "@/components/ui/form";
import InputForm from "@/components/forms/Input";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popoverDialog";
import { format } from "date-fns";
import { cn } from "@/lib/utils";
import { Calendar } from "@/components/ui/calendar";
import { Button } from "@/components/ui/button";
import { useToast } from "@/components/ui/use-toast";
import { MedicalHistory } from "@/types";

type Props = {
  petId: number;
  medicalHistoryId?: number;
  existingData?: MedicalHistory;
  onCloseDialog: any;
  create?: boolean;
};

function MedicalHistoryForm({
  petId,
  medicalHistoryId,
  existingData,
  onCloseDialog,
  create,
}: Props) {
  const { toast } = useToast();

  const token = useStore((state) => state.token);
  const axiosI = createInstance(token());

  const queryClient = useQueryClient();

  const formSchema = z.object({
    date: z.any(),
    description: z.string().min(5).max(50000),
    treatment: z.string().min(5).max(1000),
    vetName: z.string().min(5).max(100),
    petId: z.coerce.number().min(1),
  });

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: existingData,
  });

  const submitMedicalHistoryMutation = async (
    data: z.infer<typeof formSchema>,
  ) => {
    if (create == true) {
      return await axiosI.post(`/api/medicalhistory/`, data);
    } else {
      return await axiosI.put(`api/medicalhistory/${medicalHistoryId}`, data);
    }
  };

  const { mutate: submitMedicalHistory, isLoading } = useMutation({
    mutationFn: async (data: z.infer<typeof formSchema>) =>
      submitMedicalHistoryMutation(data),
    onSuccess: () => {
      create == true
        ? toast({ description: "Historial médico agregado" })
        : toast({ description: "Historial médico modificado" });
      queryClient.invalidateQueries(["medicalHistory"]);
      form.reset(existingData);
      onCloseDialog();
    },
    onError: (err: any) => {
      if (err.response.status === 401) toast({ description: "U" });
      toast({ description: "Error, intentá de nuevo" });
    },
  });

  const onSubmit = (data: z.infer<typeof formSchema>) => {
    submitMedicalHistory(data);
  };

  useEffect(() => {
    form.setValue("petId", petId);
  }, [petId, form]);

  useEffect(() => {
    if (!open) {
      form.reset(existingData);
    }
  }, [open, form, existingData]);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)}>
        <InputForm form={form} label="description" name="description" />
        <InputForm form={form} label="treatment" name="treatment" />
        <InputForm form={form} label="vetName" name="vetName" />
        <FormField
          control={form.control}
          name="petId"
          render={({ field }) => (
            <input type="hidden" {...field} value={petId} readOnly />
          )}
        />
        <div className="flex gap-4">
          <div>
            <FormField
              control={form.control}
              name="date"
              render={({ field }) => (
                <FormItem className="flex flex-col">
                  <div>
                    <Popover>
                      <PopoverTrigger asChild>
                        <FormControl>
                          <Button
                            variant={"outline"}
                            className={cn(
                              "w-[240px] pl-3 text-left font-normal",
                              !field.value && "text-muted-foreground",
                            )}
                          >
                            {field.value ? (
                              format(field.value, "PPP")
                            ) : (
                              <span>Fecha</span>
                            )}
                          </Button>
                        </FormControl>
                      </PopoverTrigger>
                      <PopoverContent className="w-auto p-0" align="start">
                        <Calendar
                          mode="single"
                          selected={field.value}
                          onSelect={field.onChange}
                          disabled={(date) =>
                            date > new Date() || date < new Date("1900-01-01")
                          }
                          initialFocus
                        />
                      </PopoverContent>
                    </Popover>
                  </div>

                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
          <div>
            <div className="mb-3 block basis-1/3"></div>
          </div>
        </div>
        <div>
          <Button isLoading={isLoading} disabled={isLoading} type="submit">
            Crear
          </Button>
        </div>
      </form>
    </Form>
  );
}

export default MedicalHistoryForm;
