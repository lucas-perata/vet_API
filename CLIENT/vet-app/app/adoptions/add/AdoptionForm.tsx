"use client"

import { SelectAdoptions } from '@/components/forms/SelectAdoptions';
import { useToast } from '@/components/ui/use-toast';
import useStore from '@/store/store';
import { createInstance } from '@/utils/axiosConfig';
import { BsAsAreasLists, ProvincesList } from '@/utils/lists';
import { zodResolver } from '@hookform/resolvers/zod';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import React, { useState } from 'react'
import { useForm } from 'react-hook-form';
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Checkbox } from "@/components/ui/checkbox"
import { z } from 'zod';
import { getAreasByProvince } from '@/utils/filterProvincesZones';
import CheckboxForm from '@/components/forms/Checkbox';
import InputForm from '@/components/forms/Input';
import { Popover, PopoverContent, PopoverTrigger } from '@/components/ui/popover';
import { Button } from '@/components/ui/button';
import { cn } from '@/lib/utils';
import { format } from 'date-fns';
import { LuCake } from 'react-icons/lu';
import { Calendar } from '@/components/ui/calendar';

export default function AdoptionForm() {

  // filter areas and provinces 
  const [selectedProvince, setSelectedProvince] = useState("");
  const [areas, setAreaList] = useState([]);

  const { toast } = useToast();

  const token = useStore((state) => state.token);
  const axiosI = createInstance(token());

  const queryClient = useQueryClient();

  const { formState: { isSubmitting, isValid, isDirty, errors }, } = useForm();

  const formSchema = z.object({
    area: z.string().min(1).max(25),
    province: z.string().min(5).max(25),
    statusList: z.string(),
    isNeutered: z.boolean().default(false),
    isVaccinated: z.boolean().default(false),
    isDeworm: z.boolean().default(false),
    gender: z.string(),
    name: z.string(),
    weight: z.coerce.number(),
    breed: z.string(),
    dateOfBirth: z.date()
  })

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
  });

  const submitAdoptionMutation = async (data: z.infer<typeof formSchema>) => {
    return await axiosI.post("/api/adoption", data);
  }

  const { mutate: submitAdoption, isLoading } = useMutation({
    mutationFn: async (data: z.infer<typeof formSchema>) =>
      submitAdoptionMutation(data),
    onSuccess: () => {
      toast({ description: "Agregado" });
      queryClient.invalidateQueries(["adoptions"]);
    },
    onError: () => {
      toast({ description: "Error" });
    }
  })

  const onSubmit = (data: z.infer<typeof formSchema>) => {
    submitAdoption(data);
  }


  return (
    <Form {...form}>
      <form className="flex flex-col mt-3 gap-4" onSubmit={form.handleSubmit(onSubmit)}>
        <div>
          <SelectAdoptions label='Provincia' name="province" form={form} selection={ProvincesList} onValueChange={(value) => {
            setSelectedProvince(value);
            setAreaList(getAreasByProvince(value));
          }} />
          <SelectAdoptions label='Partido' name='area' form={form} selection={areas}
          />








        </div>
        <div>
          <InputForm form={form} label="Name" name="name" />
          <InputForm form={form} label="Gender" name="gender" />
          <div className="flex gap-4">
            <div>
              <FormField
                control={form.control}
                name="dateOfBirth"
                render={({ field }) => (
                  <FormItem className="flex flex-col">
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
                              <span>Fecha de nacimiento</span>
                            )}
                            <LuCake className="ml-auto h-4 w-4 opacity-50" />
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

                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex basis-1/2">
              <InputForm label="photo" name="photo" type="file" form={form} />
            </div>
          </div>
          <div className="flex gap-4 ">
            <CheckboxForm label="Vacunado" name="isVaccinated" form={form} />
            <CheckboxForm label="Desparasitado" name="isDeworm" form={form} />
            <CheckboxForm label="Castrado" name="isNeutered" form={form} />
          </div>
        </div>
        <div>
          <Button disabled={!isValid} type="submit" >
            Crear
          </Button>
        </div>
      </form>
    </Form>
  )
}

