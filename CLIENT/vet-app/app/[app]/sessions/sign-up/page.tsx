"use client"
import React, { useState, FormEvent } from 'react';
import { useToast } from "@/components/ui/use-toast"
import { CardTitle, CardDescription, CardHeader, CardContent, Card } from "@/components/ui/card"
import { Label } from "@/components/ui/label"

import Input from '@/components/forms/Input';

import { Button } from "@/components/ui/button"
import { Switch } from "@/components/ui/switch"
import { registerOwner, registerVet } from '@/utils/auth';
import { SelectAdoptions } from '@/components/forms/SelectAdoptions';
import { ProvincesList } from '@/utils/lists';
import { useForm } from 'react-hook-form';
import { Form } from "@/components/ui/form";
import { z } from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';
import { getAreasByProvince } from '@/utils/filterProvincesZones';
import InputForm from '@/components/forms/Input';



const page: React.FC = () => {
  const [isSwitchOn, setSwitchOn] = useState(false);

  const handleSwitchChange = () => {
    setSwitchOn(!isSwitchOn);
  };
  const [selectedProvince, setSelectedProvince] = useState("");
  const [areas, setAreaList] = useState([]);

  // TODO: add z to this form
  const { formState: { isSubmitting, isValid, isDirty, errors }, } = useForm();
  const formSchema = z.object({
    area: z.string(),
    province: z.string(),
    email: z.string().email(),
    displayName: z.string(),
    password: z.string(),
  })

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
  });

  async function onSubmit(data: z.infer<typeof formSchema>, event: React.BaseSyntheticEvent) {
    const formData = new FormData();

    formData.append("displayName", data.displayName);
    formData.append("email", data.email);
    formData.append("password", data.password);
    formData.append("province", data.province);
    formData.append("area", data.area);

    console.log([...formData]);
    if (isSwitchOn) {

      try {
        const user = await registerOwner(formData);
        toast({
          title: "Ingresaste"
        });
        window.location.href = "/app/dashboard"
      } catch (err: any) {
        setError(err.message);
        console.log(error);
        toast({
          title: "Error owner"
        })
      }

    }
    else {
      try {
        const user = await registerVet(formData);
        toast({
          title: "Ingresaste"
        });
        window.location.href = "/app/dashboard"
      } catch (err: any) {
        setError(err.message);
        console.log(error);
        toast({
          title: "Error vet"
        })
      }
    };

  }

  const [error, setError] = useState<string | null>(null);
  const { toast } = useToast()

  return (
    <>
      <p>{isSwitchOn ? 'Switch is ON' : 'Switch is OFF'}</p>
      <Switch id="registrate como veterinario" onClick={handleSwitchChange} />
      <Label htmlFor="registrate como veterinario">Registrate como veterinario</Label>

      <Form {...form} >
        <form onSubmit={form.handleSubmit(onSubmit)} >
          <Card className="mx-auto max-w-sm">
            <CardHeader className="space-y-1">
              <CardTitle className="text-2xl font-bold">Crea tu cuenta</CardTitle>
              <div className='flex justify-center align-middle'>
              </div>
            </CardHeader>
            <CardContent>
              <InputForm form={form} label="displayName" name="displayName" />
              <InputForm form={form} label="email" name="email" />
              <InputForm form={form} label="password" name="password" />
              <SelectAdoptions label='Provincia' name="province" form={form} selection={ProvincesList} onValueChange={(value) => {
                setSelectedProvince(value);
                setAreaList(getAreasByProvince(value));
              }} />
              <SelectAdoptions label='Partido' name='area' form={form} selection={areas}
              />
              <Button className="w-full" type="submit">
                Crear cuenta
              </Button>
            </CardContent>
          </Card>
        </form>
      </Form>
    </>
  );
};

export default page;


