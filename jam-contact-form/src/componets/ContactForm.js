import React, {useState} from "react";
import axios from 'axios';

function ContactForm() 
{
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [message, setMessage] = useState('');

    const resetForm = () => {
        setName('');
        setEmail('');
        setMessage('');
    };

    const onSubmit = (e) => 
    {
        e.preventDefault();
        axios(
        {
            method: "POST",
            url:"https://contactformbackendapi.azurewebsites.net/send",
            data:  {name,email,message},
            headers: ""
        })
        .then( (response) =>
        {
            console.log(response);
            if (response.data.status === 'success') 
            {
              alert("Message Sent.");
              resetForm();
            } 
            else if(response.data.status === 'fail') 
            {
              alert("Message failed to send.")
            }
        });
    };

    return (
        <form id="contact-form" onSubmit={onSubmit} method="POST">
            <div className="form-group">
                <label htmlFor="name">Name</label>
                <input type="text" className="form-control" id="name" onChange={(e) => setName(e.target.value)} />
            </div>
            <div className="form-group">
                <label htmlFor="exampleInputEmail1">Email address</label>
                <input type="text" className="form-control" id="email" aria-describedby="emailHelp" onChange={(e) => setEmail(e.target.value)} />
            </div>
            <div className="form-group">
                <label htmlFor="message">Message</label>
                <textarea className="form-control" rows="5" id="message" onChange={(e) => setMessage(e.target.value)} />
            </div>
            <button type="submit" className="btn btn-primary">Submit</button>
        </form>
    );
  }

  export default ContactForm;