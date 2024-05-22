import React from 'react'
import { UseControllerProps } from 'react-hook-form';
import {
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form"
import { Textarea } from "@/components/ui/textarea"

type Props = {
  label: string;
  name: string;
  form: any;
  placeholder: string;
} & UseControllerProps;

function TextArea(props: Props) {
  return (
    <div>
      <FormField
        control={props.form.control}
        name={props.name}
        render={({ field }) => (
          <FormItem>
            <FormControl>
              <Textarea
                placeholder={props.placeholder}
                className="resize-none"
                {...field}
              />
            </FormControl>
            <FormDescription>
            </FormDescription>
            <FormMessage />
          </FormItem>
        )}
      />
    </div>
  )
}

export default TextArea
