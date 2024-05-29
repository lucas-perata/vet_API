"use client"
import React, { useState, useEffect } from 'react';
import { Switch } from "@/components/ui/switch"
import { IoLogoAndroid } from "react-icons/io";
import { MdComputer } from "react-icons/md";
import { FaAppStoreIos } from "react-icons/fa";
import { TbGps } from "react-icons/tb";
import { z } from 'zod';
import { Button } from "@/components/ui/button"
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form"
import { Input } from "@/components/ui/input"

import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import Image from 'next/image';
import Link from 'next/link';

export default function Home() {

  const [isSwitchOn, setSwitchOn] = useState(false);
  const [showDiv, setShowDiv] = useState(false);

  const handleSwitchChange = () => {
    setSwitchOn(!isSwitchOn);
  };

  useEffect(() => {
    const handleScroll = () => {
      const scrollHeight = document.documentElement.scrollHeight;
      const scrollTop = document.documentElement.scrollTop;
      const clientHeight = document.documentElement.clientHeight;

      if (scrollTop + clientHeight >= scrollHeight * 0.8) {
        setShowDiv(true);
      } else {
        setShowDiv(false);
      }
    };

    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  const [isVisible, setIsVisible] = useState(false);

  const toggleVisibility = () => {
    if (window.pageYOffset > 300) {
      setIsVisible(true);
    }
  };

  useEffect(() => {
    window.addEventListener('scroll', toggleVisibility);

    return () => {
      window.removeEventListener('scroll', toggleVisibility);
    };
  }, []);

  const formSchema = z.object({
    email: z.string().min(4, {
      message: "Ingresa tu email.",
    }),
  })

  function onSubmit(values: z.infer<typeof formSchema>) {
    console.log(values)
  }

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      email: "",
    },
  })

  return (
    <div className="flex flex-col w-full h-full p-0 m-0">
      <div className="flex justify-center items-center  bg-green-600 text-white min-w-full h-[90vh] p-10 shadow-lg">
        <div className="flex flex-col justify-around h-full">
          <Image width={150} height={100} src="/bird.png" alt="" />
          <Image width={150} height={100} src="/dog.png" alt="" />
        </div>


        <div className="flex flex-col">
          {isSwitchOn ? <div className="flex flex-col items-center text-center gap-8">
            <h2 className="text-5xl font-bold w-[30vw]">Potenciá tu práctica profesional</h2>
            <p className="w-[35vw]">Optimiza tu flujo de trabajo con nuestra plataforma integral para veterinarios y paseadores de perros. Gestiona citas, historiales médicos y servicios de paseo.
              Mantén una comunicación fluida con tus clientes, todo en un solo lugar práctico y eficiente.</p>
          </div> : <div className="flex flex-col items-center text-center gap-8">
            <h2 className="text-5xl font-bold w-[30vw]">Cuidá tu mejor amigo como se merece</h2>
            <p className="w-[35vw]">Nuestra plataforma te facilita el cuidado de tus mascotas. Accede a veterinarios confiables, agenda citas, revisa historiales médicos y encuentra consejos expertos.
              Todo lo que necesitas para mantener a tus fieles compañeros saludables y felices, en un solo lugar conveniente.</p>
          </div>}
          <div className="flex flex-col items-center mt-4">
            <Form {...form}>
              <form onSubmit={form.handleSubmit(onSubmit)} className="flex space-y-2">
                <FormField
                  control={form.control}
                  name="email"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel></FormLabel>
                      <FormControl>
                        <Input placeholder="shadcn" {...field} />
                      </FormControl>
                      <FormMessage />
                      <FormDescription>
                        <span className="text-white text-center">
                          Reservá tu lugar para la beta
                        </span>
                      </FormDescription>
                    </FormItem>
                  )}
                />
                <Button type="submit">Unirme</Button>
              </form>
            </Form>
          </div>
          <div className="flex justify-center align-center gap-4 mt-7">
            <p>{isSwitchOn ? '' : ''}</p>
            <div className='flex justify-center align-middle'>
              <Switch id="registrate como veterinario" onClick={handleSwitchChange} />
            </div>
          </div>
        </div>

        <div className="flex flex-col justify-around h-full">
          <Image width={150} height={100} src="/vet.png" alt="" />
          <Image width={150} height={100} src="/pawlove.png" alt="" />
        </div>

      </div>

      <div className="bg-green-100 h-[80vh] shadow-lg flex flex-col justify-center items-center">
        <h2 className="text-4xl font-bold p-10 text-center">Plataforma integrada para el cuidado de mascotas</h2>
        <p className="text-center text-xl">
          Juntos podemos construir una comunidad dedicada exclusivamente al cuidado de las mascotas        </p>
        <div className="flex flex-wrap p-10 w-[60vw] gap-14 justify-center">
          {isVisible && <div className="animate-fadeIn ">
            <div className="flex text-center gap-2 p-4 flex-col rounded-2xl shadow-lg">
              <Image width={170} height={100} src="/vet.png" alt="" />
              <h3 className="font-bold text-xl">Paseadores</h3>
            </div>
          </div>}
          {isVisible && <div className="animate-fadeIn">
            <div className="flex text-center gap-2 p-4 flex-col  rounded-2xl shadow-lg">
              <Image width={170} height={100} src="/vet.png" alt="" />
              <h3 className="font-bold text-xl">Paseadores</h3>
            </div>
          </div>}
          {isVisible && <div className="animate-fadeIn">
            <div className="flex text-center gap-2 p-4 flex-col  rounded-2xl shadow-lg">
              <Image width={180} height={100} src="/vet.png" alt="" />
              <h3 className="font-bold text-xl">Paseadores</h3>
            </div>
          </div>}
          {isVisible && <div className="animate-fadeIn">
            <div className="flex text-center gap-2 p-4 flex-col  rounded-2xl shadow-lg">
              <Image width={180} height={100} src="/vet.png" alt="" />
              <h3 className="font-bold text-xl">Paseadores</h3>
            </div>
          </div>}
          {isVisible && <div className="animate-fadeIn">
            <div className="flex text-center gap-2 p-4 flex-col  rounded-2xl shadow-lg">
              <Image width={170} height={100} src="/vet.png" alt="" />
              <h3 className="font-bold text-xl">Paseadores</h3>
            </div>
          </div>}
          {isVisible && <div className="animate-fadeIn">
            <div className="flex text-center gap-2 p-4 flex-col  rounded-2xl shadow-lg">
              <Image width={170} height={100} src="/vet.png" alt="" />
              <h3 className="font-bold text-xl">Paseadores</h3>
            </div>
          </div>}
          {isVisible && <div className="animate-fadeIn">
            <div className="flex text-center gap-2 p-4 flex-col  rounded-2xl shadow-lg">
              <Image width={180} height={100} src="/vet.png" alt="" />
              <h3 className="font-bold text-xl">Paseadores</h3>
            </div>
          </div>}
          {isVisible && <div className="animate-fadeIn">
            <div className="flex text-center gap-2 p-4 flex-col  rounded-2xl shadow-lg">
              <Image width={180} height={100} src="/vet.png" alt="" />
              <h3 className="font-bold text-xl">Paseadores</h3>
            </div>
          </div>}

        </div>

      </div>
      <div className="h-[80vh] flex flex-col items-center shadow-lg bg-green-50">
        <h2 className="text-4xl font-bold p-10 text-center">Por qué elegir VET MVP</h2>

        <div className="flex justify-center shadow-lg">

          <div className='h-[55vh] w-1/2 bg-green-100 p-10 rounded-xl'>
            <h3 className="font-bold text-2xl">Profesionales</h3>
            <ul className="flex flex-col justify-around h-72">
              <li>
                <span className="flex">
                  <TbGps />
                  Gestiona tu agenda de forma eficiente
                </span>
              </li>
              <li className="flex">
                <TbGps />
                Promociona tu negocio sin esfuerzo</li>
              <li className="flex">
                <TbGps />
                Accede a los registros clínicos de tus pacientes</li>
            </ul>
          </div>
          <div className="bg-green-200 w-1/2 p-10 rounded-xl">
            <h3 className="font-bold text-2xl">Dueños</h3>
            <ul className="flex flex-col justify-around h-72">
              <li className="flex">
                <TbGps />
                Accede a una amplia red de profesionales en tu zona
              </li>
              <li className="flex">
                <TbGps />
                Agenda citas fácilmente desde la plataforma
              </li>
              <li className="flex">
                <TbGps />
                Mantén en un solo lugar el historial médico de tus mascotas
              </li>
            </ul>
          </div>
        </div>

        <div className="p-10">
          <Link href="/">
            <Button disabled><span className="text-lg">Quiero saber más</span></Button>
          </Link>
        </div>

      </div>

      <div className="bg-white h-[50vh] flex flex-row justify-center shadow-lg">
        <h3 className="font-bold text-4xl p-10">¿Te sentís identificado con alguna?</h3>

      </div>

      <div className="bg-green-500 h-[80vh] flex flex-row justify-center shadow-lg">
        {isVisible && <div className="bg-white self-center p-10 mt-10 mb-10 rounded-xl shadow-xl w-[60vw] h-[70vh] animate-fadeIn">

          <div className="flex flex-col items-center">
            <img width={400} height={400} src="https://media.istockphoto.com/id/1320254199/es/vector/una-mujer-divirti%C3%A9ndose-jugando-con-su-gato-y-su-perro.jpg?s=2048x2048&w=is&k=20&c=dnHe-jf3TcwBbCeXPmaX6R8gVgUxTlZy7rmOBERWgOw=" alt="" />
            <div className="flex gap-4 flex-col items-center">

              <h3 className="font-bold text-2xl text-center">Creamos una plataforma para ayudar a los amantes de las mascotas y a los veterinarios a conectar</h3>
              <p className="w-[40vw] text-center">
                Lorem ipsum dolor sit amet consectetur adipiscing elit, nulla purus facilisis sollicitudin dictumst integer laoreet varius Ultricies
                imperdiet dictum scelerisque mollis faucibus odio, pellentesque sodales luctus inceptos ligula donec libero eleifend velit,
                elementum vehicula neque netus blandit a parturient quam. Tortor in nisl neque lobortis augue quis quisque
              </p>
            </div>

          </div>

        </div>}
      </div>
      <div className="flex justify-around bg-green-50 p-10 shadow-lg">
        <div><h2 className="text-gray-400 mb-3">Servicios</h2>
          <ul>
            <li>Servicio 1</li>
            <li>Servicio 2</li>
            <li>Servicio 3</li>
          </ul>
        </div>
        <div><h2 className="text-gray-400 mb-3">Soluciones</h2>
          <ul>
            <li>Soluciones 1</li>
            <li>Soluciones 2</li>
            <li>Soluciones 3</li>
          </ul>
        </div>
        <div><h2 className="text-gray-400 mb-3">Nosotros</h2>
          <ul>
            <li>Misión            </li>
            <li>Objetivos</li>
            <li>Blog</li>
          </ul>
        </div>
        <div><h2 className="text-gray-400 mb-3">Contacto</h2>
          <ul>
            <li>Telefono</li>
            <li>Mail</li>
            <li>Redes</li>
          </ul>
        </div>
      </div>
    </div >
  );
}
