import React, {useState} from "react";
import axios from 'axios';

function ContactForm() 
{
    const [name, setName] = useState("Dan");
    const [email, setEmail] = useState("my@mail.com");
    const [message, setMessage] = useState("cum");

    const onSubmit = (e) => 
    {
        e.preventDefault();
        
        axios(
        {
            method: "POST",
            url:"http://localhost:3002/send",
            data:  {name,email,message}
        })
        .then( (response) =>
        {
            if (response.data.status === 'success') 
            {
              alert("Message Sent.");
              this.resetForm()
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
                <input type="text" className="form-control" id="name" value={name} onChange={(e) => setName(e.value)} />
            </div>
            <div className="form-group">
                <label htmlFor="exampleInputEmail1">Email address</label>
                <input type="email" className="form-control" id="email" aria-describedby="emailHelp" value={email} onChange={(e) => setEmail(e.value)} />
            </div>
            <div className="form-group">
                <label htmlFor="message">Message</label>
                <textarea className="form-control" rows="5" id="message" value={message} onChange={(e) => setMessage(e.value)} />
            </div>
            <button type="submit" className="btn btn-primary">Submit</button>
        </form>
    );
  }

  export default ContactForm;