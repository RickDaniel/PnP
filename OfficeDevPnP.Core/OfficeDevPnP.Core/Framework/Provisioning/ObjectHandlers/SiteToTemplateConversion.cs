﻿using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers
{
    public class SiteToTemplateConversion
    {
        /// <summary>
        /// Actual implementation of extracting configuration from existing site.
        /// </summary>
        /// <param name="web"></param>
        /// <param name="hiddenObjects"></param>
        /// <returns></returns>
        public ProvisioningTemplate GetRemoteTemplate(Web web, ProvisioningTemplate baseTemplate)
        {
            // Create empty object
            ProvisioningTemplate template = new ProvisioningTemplate();

            // Get Security
            template = new ObjectSiteSecurity().CreateEntities(web, template);
            // Site Fields
            template = new ObjectField().CreateEntities(web, template);
            // Content Types
            template = new ObjectContentType().CreateEntities(web, template);
            // Get Lists 
            template = new ObjectListInstance().CreateEntities(web, template);
            // Get custom actions
            template = new ObjectCustomActions().CreateEntities(web, template);
            // Get features
            template = new ObjectFeatures().CreateEntities(web, template);
            // Get composite look
            template = new ObjectComposedLook().CreateEntities(web, template);
            // Get files
            template = new ObjectFiles().CreateEntities(web, template);
            // Get Property Bag Entires
            template = new ObjectPropertyBagEntry().CreateEntities(web, template);
            // In future we could just instantiate all objects which are inherited from object handler base dynamically 

            // If a base template is specified then use that one to "cleanup" the generated template model
            if (baseTemplate != null)
            {
                //todo
            }

            return template;
        }

        public ProvisioningTemplate GetRemoteTemplate(Web web)
        {
            // Load the base template which will be used for the comparison work
            ProvisioningTemplate baseTemplate = web.GetBaseTemplate();

            return GetRemoteTemplate(web, baseTemplate);
        }

        /// <summary>
        /// Actual implementation of the apply templates
        /// </summary>
        /// <param name="web"></param>
        /// <param name="template"></param>
        public void ApplyRemoteTemplate(Web web, ProvisioningTemplate template)
        {
            // Site Security
            new ObjectSiteSecurity().ProvisionObjects(web, template);

            // Site Fields
            new ObjectField().ProvisionObjects(web, template);

            // Content Types
            new ObjectContentType().ProvisionObjects(web, template);

            // Lists
            new ObjectListInstance().ProvisionObjects(web, template);

            // Custom actions
            new ObjectCustomActions().ProvisionObjects(web, template);

            // Features
            new ObjectFeatures().ProvisionObjects(web, template);

            // Composite look 
            new ObjectComposedLook().ProvisionObjects(web, template);

            // Files
            new ObjectFiles().ProvisionObjects(web, template);

            // Property Bag Entries
            new ObjectPropertyBagEntry().ProvisionObjects(web, template);


            // Extensibility Provider CallOut the last thing we do.
            new ObjectExtensibilityProviders().ProvisionObjects(web, template);
        }
    }
}