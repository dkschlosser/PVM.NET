﻿using System;
using System.Collections.Generic;
using log4net;
using PVM.Core.Data.Proxy;
using PVM.Core.Definition;
using PVM.Core.Plan;

namespace PVM.Core.Runtime
{
    public class WorkflowInstance
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (WorkflowInstance));
        private IExecution rootExecution;

        public WorkflowInstance(IWorkflowDefinition definition)
            : this(Guid.NewGuid().ToString(), new Execution(Guid.NewGuid() + "_" + definition.InitialNode.Name, new ExecutionPlan(definition)))
        {
        }

        public WorkflowInstance(String identifier, IExecution rootExecution)
        {
            Identifier = identifier;
            this.rootExecution = rootExecution;
        } 

        public string Identifier { get; private set; }

        public bool IsFinished
        {
            get { return rootExecution.Plan.IsFinished; }
        }

        public void Start<T>(T data) where T : class
        {
            rootExecution.Start(rootExecution.Plan.Definition.InitialNode, DataMapper.ExtractData(data));
        }

        public void Start()
        {
            rootExecution.Start(rootExecution.Plan.Definition.InitialNode, new Dictionary<string, object>());
        }
    }
}