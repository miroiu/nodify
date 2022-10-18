namespace Nodifier
{
    public partial class GraphNode
    {
        public class WithContent<TContent> : GraphNode
        {
            public WithContent(IGraphEditor graph) : base(graph)
            {
            }

            public new TContent Content
            {
                get => (TContent)base.Content!;
                set => base.Content = value;
            }

            public new class WithFooter<TFooter> : WithContent<TContent>
            {
                public WithFooter(IGraphEditor graph) : base(graph)
                {
                }

                public new TFooter Footer
                {
                    get => (TFooter)base.Footer!;
                    set => base.Footer = value;
                }

                public new class WithHeader<THeader> : WithFooter<TFooter>
                {
                    public WithHeader(IGraphEditor graph) : base(graph)
                    {
                    }

                    public new THeader Header
                    {
                        get => (THeader)base.Header!;
                        set => base.Header = value;
                    }
                }
            }

            public new class WithHeader<THeader> : WithContent<TContent>
            {
                public WithHeader(IGraphEditor graph) : base(graph)
                {
                }

                public new THeader Header
                {
                    get => (THeader)base.Header!;
                    set => base.Header = value;
                }

                public new class WithFooter<TFooter> : WithHeader<THeader>
                {
                    public WithFooter(IGraphEditor graph) : base(graph)
                    {
                    }

                    public new TFooter Footer
                    {
                        get => (TFooter)base.Footer!;
                        set => base.Footer = value;
                    }
                }
            }
        }
    }
}
