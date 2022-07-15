namespace Nodifier
{
    public partial class GraphNode
    {
        public class WithHeader<THeader> : GraphNode
        {
            public WithHeader(IGraph graph) : base(graph)
            {
            }

            public new THeader Header
            {
                get => (THeader)base.Header!;
                set => base.Header = value;
            }

            public new class WithFooter<TFooter> : WithHeader<THeader>
            {
                public WithFooter(IGraph graph) : base(graph)
                {
                }

                public new TFooter Footer
                {
                    get => (TFooter)base.Footer!;
                    set => base.Footer = value;
                }

                public new class WithContent<TContent> : WithFooter<TFooter>
                {
                    public WithContent(IGraph graph) : base(graph)
                    {
                    }

                    public new TContent Content
                    {
                        get => (TContent)base.Content!;
                        set => base.Content = value;
                    }
                }
            }

            public new class WithContent<TContent> : WithHeader<THeader>
            {
                public WithContent(IGraph graph) : base(graph)
                {
                }

                public new TContent Content
                {
                    get => (TContent)base.Content!;
                    set => base.Content = value;
                }

                public new class WithFooter<TFooter> : WithContent<THeader>
                {
                    public WithFooter(IGraph graph) : base(graph)
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
