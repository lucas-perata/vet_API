"use client"

import { SelectAdoptions } from '@/components/forms/SelectAdoptions';
import { useToast } from '@/components/ui/use-toast';
import useStore from '@/store/store';
import { createInstance } from '@/utils/axiosConfig';
import { BreedList, BsAsAreasLists, GenderList, ProvincesList } from '@/utils/lists';
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
import TextArea from '@/components/forms/TextArea';
import { createAdoption } from '@/app/actions/petActionsCS';
import { useRouter } from "next/navigation";
import { CaretSortIcon, CheckIcon } from '@radix-ui/react-icons';
import { Command, CommandEmpty, CommandGroup, CommandInput, CommandItem } from '@/components/ui/command';
import Input from '@/components/forms/Input';

export default function AdoptionForm() {

  // filter areas and provinces 
  const [selectedProvince, setSelectedProvince] = useState("");
  const [areas, setAreaList] = useState([]);

  const { toast } = useToast();

  const token = useStore((state) => state.token);
  const axiosI = createInstance(token());

  const queryClient = useQueryClient();

  const { formState: { isSubmitting, isValid, isDirty, errors }, } = useForm();

  // TODO: Add the other fields
  const formSchema = z.object({
    area: z.string(),
    province: z.string(),
    // statusList: z.string(),
    isNeutered: z.boolean().default(false),
    isVaccinated: z.boolean().default(false),
    isDeworm: z.boolean().default(false),
    gender: z.string(),
    name: z.string(),
    weight: z.coerce.number(),
    breed: z.string(),
    dateOfBirth: z.date(),
    description: z.string(),
    color: z.string(),
    photo: z.any(),
  })

  const router = useRouter();

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
  });

  // const submitAdoptionMutation = async (data: z.infer<typeof formSchema>) => {
  //   return await axiosI.post("/api/adoption", data);
  // }
  //
  // const { mutate: submitAdoption, isLoading } = useMutation({
  //   mutationFn: async (data: z.infer<typeof formSchema>) =>
  //     submitAdoptionMutation(data),
  //   onSuccess: () => {
  //     toast({ description: "Agregado" });
  //     queryClient.invalidateQueries(["adoptions"]);
  //   },
  //   onError: () => {
  //     toast({ description: "Error" });
  //   }
  // })

  function onSubmit(data: z.infer<typeof formSchema>, event: React.BaseSyntheticEvent) {
    const fileInput = event.target.elements.photo;
    const file = fileInput.files?.[0];

    if (file) {
      console.log(file instanceof File); // Should log true
      console.log(Object.prototype.toString.call(file));
    }
    const formData = new FormData();
    formData.append('photo', file);

    Object.entries(data).forEach(([key, value]) => {
      if (value !== null && value !== undefined) {
        if (key === 'isNeutered' || key === 'isVaccinated' || key === 'isDeworm') {
          formData.append(key, value ? 'true' : 'false'); // Convert boolean to string
        } else if (key === 'dateOfBirth') {
          formData.append(key, value.toISOString()); // Convert date to ISO string
        } else {
          formData.append(key, value.toString());
        }
      }
    });

    createAdoption(formData).then((response) => {
      console.log(response.id);
      router.push(`/adoptions`);
    });
  }


  return (
    <Form {...form}>
      <form className="flex flex-col mt-3 gap-4" onSubmit={form.handleSubmit(onSubmit)} encType='multipart/form-data'>
        <div className="flex gap-4">
          <Input form={form} label='photo' name='photo' type="file" />
          <SelectAdoptions label='Provincia' name="province" form={form} selection={ProvincesList} onValueChange={(value) => {
            setSelectedProvince(value);
            setAreaList(getAreasByProvince(value));
          }} />
          <SelectAdoptions label='Partido' name='area' form={form} selection={areas}
          />
        </div>
        <div className='flex flex-col gap-4'>

          <InputForm form={form} label="Name" name="name" />
          <SelectAdoptions form={form} label="Gender" name="gender" selection={GenderList} />
          <InputForm form={form} label="Peso" name="weight" type="number" />
          <InputForm form={form} label="Color" name="color" />
          <TextArea label="description" name="description" form={form} placeholder='test' />

          <FormField
            control={form.control}
            name="breed"
            render={({ field }) => (
              <FormItem className="">
                <Popover>
                  <PopoverTrigger asChild>
                    <FormControl>
                      <Button
                        variant="outline"
                        role="combobox"
                        className={cn(
                          "w-[400px] justify-between",
                          !field.value && "text-muted-foreground",
                        )}
                      >
                        {field.value
                          ? BreedList.find(
                            (breed) => breed.value === field.value,
                          )?.label
                          : "Selecciona la raza"}
                        <CaretSortIcon className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                      </Button>
                    </FormControl>
                  </PopoverTrigger>
                  <PopoverContent className="w-[200px] p-0">
                    <Command>
                      <CommandInput
                        placeholder="Buscar raza..."
                        className="h-9"
                      />
                      <CommandEmpty>No se encontró una raza</CommandEmpty>
                      <CommandGroup>
                        {BreedList.map((breed) => (
                          <CommandItem
                            value={breed.label}
                            key={breed.value}
                            onSelect={() => {
                              form.setValue("breed", breed.value);
                            }}
                          >
                            {breed.label}
                            <CheckIcon
                              className={cn(
                                "ml-auto h-4 w-4",
                                breed.value === field.value
                                  ? "opacity-100"
                                  : "opacity-0",
                              )}
                            />
                          </CommandItem>
                        ))}
                      </CommandGroup>
                    </Command>
                  </PopoverContent>
                </Popover>
                <FormMessage />
              </FormItem>
            )}
          />

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
            </div>
          </div>
          <div className="flex gap-4 ">
            <CheckboxForm label="Vacunado" name="isVaccinated" form={form} />
            <CheckboxForm label="Desparasitado" name="isDeworm" form={form} />
            <CheckboxForm label="Castrado" name="isNeutered" form={form} />
          </div>
        </div>
        <div>
          <Button type="submit" >
            Crear
          </Button>
        </div>
      </form>
    </Form>
  )
}

